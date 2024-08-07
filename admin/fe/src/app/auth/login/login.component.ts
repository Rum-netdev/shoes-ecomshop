import { Component, ViewChild, ViewContainerRef } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { AuthService } from "../../shared/auth.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
})
export class LoginComponent {
    
  @ViewChild('modal', {read: ViewContainerRef}) 
  entry!: ViewContainerRef;
  sub!: Subscription;

  errorMessage: string = "";
  loginForm: FormGroup;

  constructor(
    private router: Router,
    private authService: AuthService,
    // private modalService: ModalService,
    private fb: FormBuilder
  ) {
    // this.api = new Api(injector.get(HttpClient))
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      rememberMe: [false, [Validators.required]]
    });
  }

  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }

  get rememberMe() {
    return this.loginForm.get('rememberMe');
  }

  ngOnInit(): void {
    if(this.authService.isLoggedIn())
      this.router.navigate(["/"]);
  }

  ngOnDestroy(): void {
    if(this.sub) this.sub.unsubscribe();
  }

  login() {
    if(this.loginForm.invalid) {
      console.error('Invalid submit data');
      return;
    }
    console.log(this.loginForm.value);
    // const loginForm = $('#loginForm');
    // loginForm.validate();
    this.authService.login(this.loginForm.value).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (error) => {
        // this.createModal('Alert', error.message);
        console.error(error.message);
      }
    })
  }

//   createModal(title: string, message: string) {
//     this.sub = this.modalService
//       .openModal(this.entry, title, message, ModalButtonTypeEnum.OkCancel)
//       .subscribe((v) => {});
//     $('.modal').show();
//   }
}