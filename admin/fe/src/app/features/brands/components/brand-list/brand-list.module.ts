import { NgModule } from "@angular/core";
import { BrandListComponent } from "./brand-list.component";
import { RouterModule, Routes } from "@angular/router";
import { ModalService } from "../../../../shared/modal.service";
import { BrandService } from "../services/brand.services";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

const routes : Routes = [
    {path: '', component: BrandListComponent}
]
@NgModule({
    declarations: [BrandListComponent],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        FormsModule,
    ],
    exports: [
        RouterModule
    ],
    providers: [ModalService, BrandService]
})
export class BrandListModule {}