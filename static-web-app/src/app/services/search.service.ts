import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { SearchApiWebUrl } from "../app.configuration";
import { HttpClient } from "@angular/common/http";
import { SearchRequest } from "../model/search-request";
import { SearchDataResponse } from "../model/search-data-response";
import { SearchEngineResponse } from "../model/search-engine-response";
import { ApiResponse } from "./api-response";
import { catchError, map } from "rxjs/operators";
import { Observable, throwError } from "rxjs";

@Injectable({ providedIn: 'root' })
export class SearchService {
  constructor(private http: HttpClient, private toastrService: ToastrService) {}
  
  getSearchData(
    request: SearchRequest
  ): Observable<SearchDataResponse> {
    let searchUrl = `${SearchApiWebUrl}/Search/Rankings?url=${request.url}&searchTerm=${request.searchTerm}&usePlaywright=${request.usePlaywright}`;
    
    return this.http
      .get<ApiResponse>(searchUrl, { withCredentials: true })
      .pipe(  
        map((response: ApiResponse) => {
          if (response.data != null) {
            if (response.message && response.message.length > 0) {
              console.log(response.message);
            } 
            return response as SearchDataResponse;
          } else {
            if (response.message.length > 0) {
              this.toastrService.error(response.message, "", {
                positionClass: "toast-top-full-width"
              });
            }
            throw new Error(`${searchUrl} response was not successful.`);
          }
        }),
        catchError((err) => this.handleError(err))
      );
  }

  getSearchEngine(): Observable<SearchEngineResponse> {
    let searchUrl = `${SearchApiWebUrl}/Search/EngineType`;
    
    return this.http
      .get<ApiResponse>(searchUrl, { withCredentials: true })
      .pipe(  
        map((response: ApiResponse) => {
          if (response.data != null) {
            if (response.message && response.message.length > 0) {
              console.log(response.message);
            } 
            return response as SearchEngineResponse;
          } else {
            if (response.message.length > 0) {
              this.toastrService.error(response.message, "", {
                positionClass: "toast-top-full-width"
              });
            }
            throw new Error(`${searchUrl} response was not successful.`);
          }
        }),
        catchError((err) => this.handleError(err))
      );
  }

  handleError(err: any) : Observable<never>  {
    console.error(err);
    return throwError(() => err);
  }
}
