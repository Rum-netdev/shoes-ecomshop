import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class JwtService {
    getToken = () => localStorage.getItem('jwtToken') ?? null;
    saveToken = (token: string) => localStorage.setItem('jwtToken', token);
    destroyToken = () => localStorage.removeItem('jwtToken');
}