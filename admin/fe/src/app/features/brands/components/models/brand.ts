import { BaseDataResult, BaseResult } from "../../../../shared/services/models/base-result"
import { PaginationResult } from "../../../../shared/services/models/pagination"

export interface Brand {
    id: number,
    name: string,
    description: string
}

export interface GetAllBrandsQueryResult extends PaginationResult<Brand> {
}

export interface CreateBrandCommand {
    name?: string,
    description?: string
}

export interface CreateBrandCommandResult extends BaseResult {
    brandId: number
}


export interface DeleteBrandCommand {
    brandId: string
}

export interface DeleteBrandCommandResult extends BaseResult {
    brandId: string
}

export interface GetBrandByIdQueryResult extends BaseDataResult<Brand> {}

export interface UpdateBrandCommand {
    brandId: number,
    name: string,
    description: string
}

export interface UpdateBrandCommandResult extends BaseResult {
    brandId: string
}