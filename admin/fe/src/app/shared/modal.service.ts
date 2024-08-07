import { ComponentFactoryResolver, ComponentRef, Injectable, ViewContainerRef } from "@angular/core";
import { ModalComponent } from "./components/modal/modal.component";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ModalService {

    componentRef: ComponentRef<ModalComponent>;
    componentSubscribers: Subject<string>;
    constructor(
        private factoryResolver: ComponentFactoryResolver
    ) 
    {
    }

    createModal(entry: ViewContainerRef, title: string) {
        let factory = this.factoryResolver.resolveComponentFactory(ModalComponent);
        this.componentRef = entry.createComponent(factory);
        this.componentRef.instance.title = title;
        this.componentRef.instance.CloseModalEvent.subscribe(() => this.closeModal());
        this.componentSubscribers = new Subject<string>();
        return this.componentSubscribers.asObservable();
    }

    closeModal() {
        this.componentSubscribers.complete();
        this.componentRef.destroy();
    }
}