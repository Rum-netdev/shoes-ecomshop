import { NgModule } from "@angular/core";
import { LoginComponent } from "./login.component";
import { RouterModule, Routes } from "@angular/router";
import { AuthService } from "../../shared/auth.service";
import { ReactiveFormsModule } from "@angular/forms";
import { ModalService } from "../../shared/modal.service";

const routes: Routes = [
    {path: '', component: LoginComponent}
]
@NgModule({
    declarations: [LoginComponent],
    imports: [
        RouterModule.forChild(routes),
        ReactiveFormsModule
    ],
    providers: [
        AuthService,
        ModalService
    ],
    exports: [RouterModule]
})
export class LoginModule {}