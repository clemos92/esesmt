import { BasePaginationFilter } from "src/app/core/interfaces/base-pagination-filter";

export interface ChecklistTypeFilter extends BasePaginationFilter {
    name: string, 
    isActive?: boolean 
}
