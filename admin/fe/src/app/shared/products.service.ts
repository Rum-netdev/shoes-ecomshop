import { Injectable } from "@angular/core";
import { Api } from "./services/api";
import { Urls } from "./services/urls";

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    constructor(private api: Api) {}

    getAllProducts() {
        return this.api.getWithParams(Urls.products.getAll);
    }
}