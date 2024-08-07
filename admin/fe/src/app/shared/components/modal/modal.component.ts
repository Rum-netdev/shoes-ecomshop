import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html'
})
export class ModalComponent {
    @Input({required: true}) title: string;
    @Output() CloseModalEvent = new EventEmitter<any>();

    close() {

    }
}