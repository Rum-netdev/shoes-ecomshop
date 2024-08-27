import { Injectable } from "@angular/core";
import { Api } from "../../../../shared/services/api";
import { Urls } from "../../../../shared/services/urls";
import { CreateBrandCommand, CreateBrandCommandResult, DeleteBrandCommand, DeleteBrandCommandResult, GetAllBrandsQueryResult, GetBrandByIdQueryResult, UpdateBrandCommand, UpdateBrandCommandResult } from "../models/brand";

@Injectable()
export class BrandService {
    constructor(private api: Api) {}

    getAll() {
        return this.api.req<GetAllBrandsQueryResult>('get', Urls.brands.getAll);
    }

    createBrand(model: CreateBrandCommand) {
        return this.api.post<CreateBrandCommandResult>(Urls.brands.create, model);
    }

    deleteBrandById(brandId: string) {
        const removeBrandDto: DeleteBrandCommand = {brandId: brandId}; 
        return this.api.deleteWithParams<DeleteBrandCommandResult>(Urls.brands.delete, removeBrandDto);
    }

    getBrandById(brandId: string) {
        return this.api.getWithParams<GetBrandByIdQueryResult>(Urls.brands.getById.replace("{id}", brandId));
    }

    updateBrand(brand: UpdateBrandCommand) {
        return this.api.postWithParams<UpdateBrandCommandResult>(Urls.brands.update.replace("{id}", brand.brandId.toString()), brand)
    }
}