import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { ChecklistType } from '../models/checklist-type';
import { Observable } from 'rxjs';
import { PagedReponse } from 'src/app/core/interfaces/paged-reponse';
import { ChecklistTypeFilter } from '../custom-filters/checklist-type-filter';
import { DropdownDefault } from '../models/dropdown-default';

@Injectable({ providedIn: 'root' })
export class ChecklistTypeService  {

    private base: string = `${environment.apiUrl}/ChecklistType`;

    constructor(protected http: HttpClient) { }
    
    getPagedByFilter(filter: ChecklistTypeFilter): Observable<PagedReponse<ChecklistType>> {
        return this.http.post<PagedReponse<ChecklistType>>(`${this.base}/GetPagedByFilter`, filter);
    }
    
    getDetails(id: number): Observable<ChecklistType> {
        return this.http.get<ChecklistType>(`${this.base}/GetDetails/${id}`);
    }

    create(entity: ChecklistType): Observable<ChecklistType> {
        return this.http.post<ChecklistType>(`${this.base}/Save`, entity);
    }

    edit(entity: ChecklistType): Observable<ChecklistType> {
        return this.http.post<ChecklistType>(`${this.base}/Update`, entity);
    }
    
    getAllActiveToDropdown() {
        return this.http.get<DropdownDefault[]>(`${this.base}/GetAllActive`);
    }
}