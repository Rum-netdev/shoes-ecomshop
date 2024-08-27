import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Inject, TemplateRef, viewChild, ViewChild } from "@angular/core";
import { ModalService } from "../../../../shared/modal.service";
import { BrandService } from "../services/brand.services";
import { DOCUMENT } from "@angular/common";
import { Brand, CreateBrandCommand } from "../models/brand";
import { Router } from "@angular/router";
import { delay, Observable, of, tap } from "rxjs";

@Component({
    selector: 'app-brand-list',
    templateUrl: './brand-list.component.html',
    styleUrl: './brand-list.component.css'
})
export class BrandListComponent implements AfterViewInit {
    // @ViewChild('upsertModalTemplate', {read: TemplateRef}) upsertModalTemplateRef : TemplateRef<any>;
    @ViewChild('upsertModal') upsertModal: ElementRef | undefined;
    modalTitle: string;
    private brandCollection: Brand[] 
    brandDto: CreateBrandCommand = {name: '', description: ''};
    private modalRef: Observable<string>;
    currentBrand: Brand = {id: 0} as Brand;

    isOpenModal: boolean = false;

    constructor(
        // @Inject(DOCUMENT) private document: Document,
        private modalService: ModalService,
        private brandService : BrandService,
        private cdr: ChangeDetectorRef,
        private router: Router
    ) 
    { }

    ngAfterViewInit(): void {
        this.brandService
            .getAll()
            .subscribe({
                next: (response) => {
                    this.brandCollection = response.data
                    console.log(this.brandCollection);
                    this.initialBrandDataTable();
                },
                error: (error) => {
                    console.error(error);
                    this.brandCollection = []
                    this.initialBrandDataTable();
                }
            });
    }

    // openModal(modalTemplate: TemplateRef<any>) {
    //     this.modalRef = this.modalService
    //         .open(modalTemplate, {title: 'Create brand'});

    //     this.modalRef.subscribe((action) => {
    //         if(action == 'afterClose') {
    //             this.brandDto.name =  $('#brandNameInput').val() as string;
    //             this.brandDto.description =  $('#descriptionTextArea').val() as string;
    //         }
    //         else if(action == 'confirm') {
    //             console.log(this.brandDto);
    //             this.createBrand();
    //         }
    //         // console.log(action);
    //     });

    //     // this.modalService
    //     //     .open(modalTemplate, {title: 'Create brand'})
    //     //     .subscribe((action) => {
    //     //         // this is executed when user clicked comfirm button
    //     //         // get data in required fields
    //     //         this.brandDto.name =  $('#brandNameInput').val() as string;
    //     //         this.brandDto.description =  $('#descriptionTextArea').val() as string;;
    //     //         console.log('modalAction', action);
    //     //         this.brandService.createBrand(this.brandDto)
    //     //             .subscribe(
    //     //                 result => {
    //     //                     // alert
    //     //                     alert("Creating brand successfully!");
    //     //                     // reload this page to get new data
    //     //                     window.location.reload();
    //     //                 },
    //     //                 err => {
    //     //                     alert("Create brand failure");
    //     //                     console.error(err);
    //     //                 }
    //     //             );
    //     //         });
    // }


    // openModal= () => this.isOpenModal = true;

    initialBrandDataTable() {
        // Ensure the entire DOM objects are loaded
        $(document).ready(() => {
            // load data into DataTable
            $('#brandListDataTable').DataTable({
                paging: true,
                searching: true,
                ordering: true,
                data: this.brandCollection,
                columns: [
                    {data: 'id'},
                    {data: 'name'},
                    {data: 'description'},
                ],
                columnDefs: [
                    {
                        targets: 3,
                        render: function(data: any, type: any, row: any) {
                            // console.log(data);
                            // console.log(type);
                            // console.log(row);
                            return `
                            <a class="btn btn-secondary edit-btn" data-id="${row.id}">Edit</a>
                            <a class="btn btn-danger delete-btn" data-id="${row.id}">Remove</a>
                            `;
                        },
                        searchable: false
                    }
                ]
            });

            $('#brandListDataTable tbody .delete-btn').on('click', (event) => {
                // console.log("Remove brand id:" + brandId);
                const brandId = $(event.currentTarget).data('id');
                this.removeBrand(brandId);
            });

            $('#brandListDataTable tbody').on('click', '.edit-btn', (event) => {
                const brandId = $(event.currentTarget).data('id');
                const brand = this.brandCollection.find(t => t.id == brandId);
                this.onUpdateBrand(brand as Brand);
                // this.brandService.getBrandById(brandId)
                //     .subscribe(
                //         result => {
                //             // this.currentBrand = result.data;
                //             // this.cdr.detectChanges();
                //             // console.log(this.currentBrand);

                //             if(this.currentBrand)
                //                 console.log(this.currentBrand);
                //             else 
                //                 console.log('No have value');

                //             this.modalRef = this.modalService
                //                 .open(this.upsertModalTemplateRef, {title: "Update modal"});
                //             this.modalRef.subscribe((action) => {
                //                 if(action == 'confirm') {
                //                     console.log(this.currentBrand);
                //                 }
                //             });
                //         }
                //     );
            })
        });
    }

    openModal() {
        this.isOpenModal = true;
        this.cdr.detectChanges();
        $('#upsertModal').ready(() => {
            $('#upsertModal').modal('show')
            // const modal = document.getElementById('upsertModal');
            // console.log(modal);
            // if(modal != null) {
            //     modal.style.display = 'block';
            // }
        });
    }

    closeModal() {
        this.isOpenModal = false;
        this.currentBrand = {} as Brand;
        console.log(this.currentBrand);
        if(this.upsertModal?.nativeElement.style.display != null) {
            this.upsertModal.nativeElement.style.display = 'none';  // convert to display none
        }
    }

    onUpdateBrand(brand: Brand) {
        this.currentBrand = brand;
        this.openModal();
    }


    removeBrand(brandId: any) {
        this.brandService.deleteBrandById(brandId)
            .subscribe(
                result => {
                    alert("Delete brand successfully");
                    window.location.reload();
                },
                error => {
                    console.log(error);
                }
            )
    }

    // updateBrand(brandId: any) {
    //     this.brandService.getBrandById(brandId)
    //             .subscribe(
    //                 success => {
    //                     console.log(success);
    //                 }
    //             );
    // }

    updateBrand() {
        // this.brandService.getBrandById(brandId)
        //         .subscribe(
        //             success => {
        //                 console.log(success);
        //             }
        //         );

        this.brandService
            .updateBrand(
                {
                    brandId: this.currentBrand.id,
                    name: this.currentBrand.name,
                    description: this.currentBrand.description
                })
            .subscribe(
                success => {
                    alert("Update brand successfully");
                    console.log(success);
                }
            )
    }

    createBrand() {
        this.brandService.createBrand(this.brandDto)
            .subscribe(
                result => {
                    // alert
                    alert("Creating brand successfully!");
                    // reload this page to get new data
                    window.location.reload();
                },
                err => {
                    alert("Create brand failure");
                    console.error(err);
                }
            );
    }
}