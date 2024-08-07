import { NgModule } from "@angular/core";
import { ProductListComponent } from "./product-list/product-list.component";
import { RouterModule, Routes } from "@angular/router";


const routes : Routes = [
  {path: '', loadChildren: () => import('./product-list/product-list.module').then(m => m.ProductListModule)}  
];

@NgModule({
    declarations: [
        ProductListComponent
    ],
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class ProductModule {}