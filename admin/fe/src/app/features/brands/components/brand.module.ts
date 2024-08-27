import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

const routes : Routes = [
    {path: '', loadChildren: () => import('./brand-list/brand-list.module').then(m => m.BrandListModule)}
]
@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class BrandModule {}