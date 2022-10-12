import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { PagedReponse } from 'src/app/core/interfaces/paged-reponse';
import { CompletedChecklistFilter } from '../custom-filters/completed-checklist-filter';
import { CompletedChecklist } from '../models/completed-checklist';

@Injectable({ providedIn: 'root' })
export class CompletedChecklistService {

    private base: string = `${environment.apiUrl}/CompletedChecklist`;

    constructor(protected http: HttpClient) { }
    
    getPagedByFilter(filter: CompletedChecklistFilter): Observable<PagedReponse<CompletedChecklist>> {
        return this.http.post<PagedReponse<CompletedChecklist>>(`${this.base}/GetPagedByFilter`, filter);
    }
    
    getDetails(id: number): Observable<CompletedChecklist> {
        return this.http.get<CompletedChecklist>(`${this.base}/GetDetails/${id}`);
    }

    create(entity: CompletedChecklist): Observable<CompletedChecklist> {
        return this.http.post<CompletedChecklist>(`${this.base}/Save`, entity);
    }

}