import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home.component";
import { NgModule } from "@angular/core";
import { AuthService } from "../shared/auth.service";

const routes: Routes = [
    {path: '', component: HomeComponent}
]

@NgModule({
    declarations: [HomeComponent],
    imports: [
        RouterModule.forChild(routes)
    ],
    providers: [
        AuthService
    ],
    exports: [RouterModule]
})
export class HomeModule {}