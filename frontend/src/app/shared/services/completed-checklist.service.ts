import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';
import { PagedReponse } from 'src/app/core/interfaces/paged-reponse';
import { CompletedChecklistFilter } from '../custom-filters/completed-checklist-filter';
import { CompletedChecklist } from '../models/completed-checklist';
import { OnlineOfflineService } from 'src/app/core/services/online-offline.service';
import Dexie from 'dexie';
import { v4 as uuid } from 'uuid';

export class OfflineRequest {
    public id: string;
    public entity: CompletedChecklist;
}

//https://offering.solutions/blog/articles/2018/11/21/online-and-offline-sync-with-angular-and-indexeddb/

@Injectable({ providedIn: 'root' })
export class CompletedChecklistService {

    private base: string = `${environment.apiUrl}/CompletedChecklist`;
    private db: any;

    constructor(protected http: HttpClient,
        private readonly onlineOfflineService: OnlineOfflineService) {
        this.registerToEvents(onlineOfflineService);
        this.createDatabase();
    }

    private createDatabase() {
        this.db = new Dexie('ESESMTLocalDatabase');
        this.db.version(1).stores({
            offlineRequests: 'id,entity'
        });
    }

    private addToIndexedDb(offlineRequest: OfflineRequest): Observable<CompletedChecklist> {
        this.db.offlineRequests
            .add(offlineRequest)
            .then(async () => {
                const allItems: OfflineRequest[] = await this.db.offlineRequests.toArray();
                console.log('saved in DB, DB is now', allItems);
            })
            .catch(e => {
                alert('Error: ' + (e.stack || e));
            });

        return of(offlineRequest.entity);
    }

    private async sendItemsFromIndexedDb() {
        const allItems: OfflineRequest[] = await this.db.offlineRequests.toArray();

        allItems.forEach((item: OfflineRequest) => {
            // send item to backend
            console.log(`send item ${item.id} to backend`);
            this.create(item.entity).subscribe(data => {                    
                this.db.offlineRequests.delete(item.id).then(() => {
                    console.log(`item ${item.id} deleted locally`);
                });
            });
        });
    }

    private registerToEvents(onlineOfflineService: OnlineOfflineService) {
        onlineOfflineService.connectionChanged.subscribe(online => {
            if (online) {
                console.log('sending all stored items');
                this.sendItemsFromIndexedDb();
            } else {
                console.log('went offline, storing in indexdb');
            }
        });
    }

    createSafe(entity: CompletedChecklist): Observable<CompletedChecklist> {
        if (!this.onlineOfflineService.isOnline) {
            let offlineRequest = new OfflineRequest();
            offlineRequest.id = uuid();
            offlineRequest.entity = entity;
            return this.addToIndexedDb(offlineRequest);
        }
        else {
            return this.create(entity);
        }
    }

    getPagedByFilter(filter: CompletedChecklistFilter): Observable<PagedReponse<CompletedChecklist>> {
        let queryParameters: string = '?';

        const properties = Object.keys(filter);
        properties.forEach(element => {
            if ((filter[element] !== '' && filter[element] !== null && filter[element] !== undefined) || filter[element] === 0) {
                queryParameters +=`${queryParameters.length > 1 ? '&' : ''}${element}=${filter[element]}`;    
            }
        });

        return this.http.get<PagedReponse<CompletedChecklist>>(`${this.base}/GetPagedByFilter${queryParameters}`);
    }

    getDetails(id: number): Observable<CompletedChecklist> {
        return this.http.get<CompletedChecklist>(`${this.base}/GetDetails/${id}`);
    }

    private create(entity: CompletedChecklist): Observable<CompletedChecklist> {
        return this.http.post<CompletedChecklist>(`${this.base}/Save`, entity);
    }

}