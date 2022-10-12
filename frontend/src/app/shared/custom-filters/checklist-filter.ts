import { BasePaginationFilter } from "src/app/core/interfaces/base-pagination-filter";

export interface ChecklistFilter extends BasePaginationFilter {
    description: string, 
    checklistTypeId: number,
    isActive?: boolean 
}
