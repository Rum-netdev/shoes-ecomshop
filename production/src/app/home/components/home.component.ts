import { AfterViewInit, Component } from "@angular/core";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements AfterViewInit {
    constructor() {}
    
    ngAfterViewInit(): void {
        throw new Error("Method not implemented.");
    }
}