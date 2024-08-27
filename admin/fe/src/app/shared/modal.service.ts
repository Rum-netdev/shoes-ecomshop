import { ComponentFactoryResolver, ComponentRef, Inject, Injectable, Injector, TemplateRef, ViewContainerRef } from "@angular/core";
import { ModalComponent } from "./components/modal/modal.component";
import { Subject } from "rxjs";
import { DOCUMENT } from "@angular/common";

@Injectable()
export class ModalService {
    // componentRef: ComponentRef<ModalComponent>;
    private modalNotifier: Subject<string>;

    constructor(
        private factoryResolver: ComponentFactoryResolver,
        private injector: Injector,
        @Inject(DOCUMENT) private document: Document
    ) 
    {
    }

    open(content: TemplateRef<any>, options: {title: string}) {
        const modalComponentFactory = this.factoryResolver.resolveComponentFactory(ModalComponent);
        const contentViewRef = content.createEmbeddedView(null);
        const modalComponent = modalComponentFactory.create(this.injector, [
            contentViewRef.rootNodes
        ]);

        modalComponent.instance.title = options.title;

        modalComponent.instance.closeEvent.subscribe(() => {
            this.closeModal();
        });
        modalComponent.instance.submitEvent.subscribe(() => {
            this.afterClose();
            this.submitModal();
        });

        modalComponent.hostView.detectChanges();
        this.document.body.appendChild(modalComponent.location.nativeElement);

        this.modalNotifier = new Subject();
        return this.modalNotifier?.asObservable();
    }

    closeModal() {
        this.modalNotifier.complete();
    }

    submitModal() {
        this.modalNotifier.next('confirm');
        this.closeModal();
    }

    afterClose() {
        this.modalNotifier.next('afterClose');
    }
}