import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { CrudService } from 'src/app/core/services/crud.service';
import { Observable } from 'rxjs';
import { PagedReponse } from 'src/app/core/interfaces/paged-reponse';
import { Checklist } from '../models/checklist';
import { ChecklistFilter } from '../custom-filters/checklist-filter';

@Injectable({ providedIn: 'root' })
export class ChecklistService extends CrudService<Checklist, number>{

    constructor(protected http: HttpClient) {
        super(http, `${environment.apiUrl}/Checklist`);
    }
    
    getPagedByFilter(filter: ChecklistFilter): Observable<PagedReponse<Checklist>> {
        return this.http.post<PagedReponse<Checklist>>(`${this._base}/GetPagedByFilter`, filter);
    }
    
    getDetails(id: number): Observable<Checklist> {
        return this.http.get<Checklist>(`${this._base}/GetDetails/${id}`);
    }

    create(entity: Checklist): Observable<Checklist> {
        return this._http.post<Checklist>(`${this._base}/Save`, entity);
    }

    edit(entity: Checklist): Observable<Checklist> {
        return this._http.post<Checklist>(`${this._base}/Update`, entity);
    }
}