import { AfterViewInit, Component } from "@angular/core";
// import $ from 'jquery';
// import'datatables.net'

@Component({
    selector: 'app-product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements AfterViewInit {
    ngAfterViewInit(): void {
        $('#dataTable').DataTable();
    }
}