import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { PagedReponse } from 'src/app/core/interfaces/paged-reponse';
import { Checklist } from '../models/checklist';
import { ChecklistFilter } from '../custom-filters/checklist-filter';
import { DropdownDefault } from '../models/dropdown-default';

@Injectable({ providedIn: 'root' })
export class ChecklistService {

    private base: string = `${environment.apiUrl}/Checklist`;
    private db: any;

    constructor(protected http: HttpClient){ }

    getPagedByFilter(filter: ChecklistFilter): Observable<PagedReponse<Checklist>> {
        let queryParameters: string = '?';

        const properties = Object.keys(filter);
        properties.forEach(element => {
            if ((filter[element] !== '' && filter[element] !== null && filter[element] !== undefined) || filter[element] === 0) {
                queryParameters +=`${queryParameters.length > 1 ? '&' : ''}${element}=${filter[element]}`;    
            }
        });

        return this.http.get<PagedReponse<Checklist>>(`${this.base}/GetPagedByFilter${queryParameters}`);
    }

    getDetails(id: number): Observable<Checklist> {
        return this.http.get<Checklist>(`${this.base}/GetDetails/${id}`);
    }

    create(entity: Checklist): Observable<Checklist> {
        return this.http.post<Checklist>(`${this.base}/Save`, entity);
    }

    edit(entity: Checklist): Observable<Checklist> {
        return this.http.post<Checklist>(`${this.base}/Update`, entity);
    }

    getAllActiveToDropdown() {
        return this.http.get<DropdownDefault[]>(`${this.base}/GetAllActive`);
    }

    getByIdToDropdown(id: number) {
        return this.http.get<DropdownDefault[]>(`${this.base}/GetByIdToDropdown/${id}`);
    }
}