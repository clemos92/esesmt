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
        let queryParameters: string = '?';

        const properties = Object.keys(filter);
        properties.forEach(element => {
            if ((filter[element] !== '' && filter[element] !== null && filter[element] !== undefined) || filter[element] === 0) {
                queryParameters +=`${queryParameters.length > 1 ? '&' : ''}${element}=${filter[element]}`;    
            }
        });

        return this.http.get<PagedReponse<ChecklistType>>(`${this.base}/GetPagedByFilter${queryParameters}`);
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