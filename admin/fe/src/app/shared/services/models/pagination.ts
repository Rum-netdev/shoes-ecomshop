import { BaseResult } from "./base-result";

export interface PaginationResult<T> extends BaseResult {
    data: T[],
    pageSize: number,
    pageCount: number,
    totalRecords: number
}