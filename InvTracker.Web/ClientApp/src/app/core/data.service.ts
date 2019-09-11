import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
//import { AppError } from '../../common/app-error';


@Injectable({
  providedIn: 'root'
})





export class DataService {
  private apiUrl: string;
  private httpDataServiceOptions: any;
  private httpHeaderFileDownload: any;
  private httpHeaderFileUpload: any;


  constructor( private http: HttpClient) {
    this.apiUrl = environment.serviceBaseUrl;
    this.httpDataServiceOptions = {
      
      headers: new HttpHeaders({
        // 'AUTHA': this.appToken.getSessionToken(),
        'Content-Type': 'application/json'
      })
     // ,withCredentials: true

    };

    this.httpHeaderFileUpload = {
      // headers: new HttpHeaders({
      //    'AUTHA': this.appToken.getSessionToken()
      // })
    };

    this.httpHeaderFileDownload = {
      // headers: new HttpHeaders({
      //    'AUTHA': this.appToken.getSessionToken()
      // }),
      responseType: 'blob'
    };
  }
  getAll(url) {
    return this.http.get(this.apiUrl + url, this.httpDataServiceOptions)
      .pipe(
        // map(response => response), catchError(this.handleError)
      );
  }
  getAllUsingPost(url, resource) {
    return this.http.post(this.apiUrl + url, resource, this.httpDataServiceOptions)
      .pipe(map(response => response), catchError(this.handleError));
  }

  get(url, id = null, param = null, param1 = null) {

    let URL = this.apiUrl + url;
    if (id != null) {
      URL += '/' + id;
    }
    if (param != null) {
      URL += '/' + param;
    }
    if (param1 != null) {
      URL += '/' + param1;
    }
    return this.http.get(URL, this.httpDataServiceOptions)
      .pipe(map(response => response), catchError(this.handleError));
  }
  create(url, resource) {
    
        return this.http.post<any>(this.apiUrl + url, resource, this.httpDataServiceOptions)
      .pipe(
        map(response => response), catchError(this.handleError)
      );
  }
  update(url, resource, parms) {
    return this.http.post(this.apiUrl + url, JSON.stringify(resource), this.httpDataServiceOptions)
      .pipe(map(response => response), catchError(this.handleError));
  }
  private handleError(error: Response) {
    return throwError(error);
  }

  postfile(url, formData) {
    return this.http.post<any>(this.apiUrl + url, formData, this.httpHeaderFileUpload)
      .pipe(
      );
  }

  DownloadFile(url) {
    return this.http.get(this.apiUrl + url, this.httpHeaderFileDownload)
      .pipe(
      );
  }

  DownloadFileWithPost(url, formData) {
    return this.http.post(this.apiUrl + url, formData, this.httpHeaderFileDownload)
      .pipe(
      );
  }

}
