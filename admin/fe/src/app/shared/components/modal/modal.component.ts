import { Component, ElementRef, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html',
    styleUrl: './modal.component.css'
})
export class ModalComponent {
    @Input({required: true}) title?: string = 'Modal title';
    @Output() closeEvent = new EventEmitter();
    @Output() afterCloseEvent = new EventEmitter();
    @Output() submitEvent = new EventEmitter();

    constructor(private elementRef: ElementRef) {

    }

    close() {
        this.closeEvent.emit();
        this.elementRef.nativeElement.remove();
    }

    afterClose() {
        this.afterCloseEvent.emit();
    }

    submit() {
        this.submitEvent.emit();
        this.elementRef.nativeElement.remove();
    }
}