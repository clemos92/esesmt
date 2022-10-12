import { BasePaginationFilter } from "src/app/core/interfaces/base-pagination-filter";

export interface CompletedChecklistFilter extends BasePaginationFilter {
    description: string, 
    checklistTypeId?: number,
    startDate: string,
    endDate: string,
}
