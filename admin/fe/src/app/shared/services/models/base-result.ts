export interface BaseResult  {
    isSucceed: boolean,
    message: string,
}

export interface BaseDataResult<T> extends BaseResult {
    data: T
}