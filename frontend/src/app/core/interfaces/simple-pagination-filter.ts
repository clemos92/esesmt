import { BasePaginationFilter } from "./base-pagination-filter";

export interface SimplePaginationFilter extends BasePaginationFilter {
    searchFilter?: string;
}