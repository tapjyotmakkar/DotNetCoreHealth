import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { share } from 'rxjs/operators';

export class ServerStatus {
    id: number = 0;
    name: string = '';
    status: string = '';
    date: string = '';
}

export class ServerStatuses {
    statuses: ServerStatus[] = [];
}

@Injectable({
    providedIn: 'root'
})
export class AppComponentService {
    constructor(private httpClient: HttpClient) { }

    public getServerStatuses(): Observable<ServerStatuses> {
        return this.httpClient.get<ServerStatuses>('http://20.193.32.243/serverstatus');
    }
}
