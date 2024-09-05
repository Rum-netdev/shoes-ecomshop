import { NgModule } from "@angular/core";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { NavbarComponent } from "./navbar/navbar.component";

@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        NavbarComponent
    ],
    exports: [
        HeaderComponent,
        FooterComponent,
        NavbarComponent
    ]
})
export class SharedModule {

}