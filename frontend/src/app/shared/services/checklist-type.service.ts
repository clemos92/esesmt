import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { CrudService } from 'src/app/core/services/crud.service';
import { ChecklistType } from '../models/checklist-type';
import { Observable } from 'rxjs';
import { PagedReponse } from 'src/app/core/interfaces/paged-reponse';
import { ChecklistTypeFilter } from '../custom-filters/checklist-type-filter';
import { DropdownDefault } from '../models/dropdown-default';

@Injectable({ providedIn: 'root' })
export class ChecklistTypeService extends CrudService<ChecklistType, number>{

    constructor(protected http: HttpClient) {
        super(http, `${environment.apiUrl}/ChecklistType`);
    }
    
    getPagedByFilter(filter: ChecklistTypeFilter): Observable<PagedReponse<ChecklistType>> {
        return this.http.post<PagedReponse<ChecklistType>>(`${this._base}/GetPagedByFilter`, filter);
    }
    
    getDetails(id: number): Observable<ChecklistType> {
        return this.http.get<ChecklistType>(`${this._base}/GetDetails/${id}`);
    }

    create(entity: ChecklistType): Observable<ChecklistType> {
        return this._http.post<ChecklistType>(`${this._base}/Save`, entity);
    }

    edit(entity: ChecklistType): Observable<ChecklistType> {
        return this._http.post<ChecklistType>(`${this._base}/Update`, entity);
    }
    
    getAllActiveToDropdown() {
        return this.http.get<DropdownDefault[]>(`${this._base}/GetAllActive`);
    }
}