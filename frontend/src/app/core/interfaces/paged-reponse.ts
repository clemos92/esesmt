export interface PagedReponse<T> {
    data: T[];

    message: string;
    succeeded: boolean;
    errors: string[];

    pageIndex: number;
    pageSize: number;
    totalRecords: number;
}