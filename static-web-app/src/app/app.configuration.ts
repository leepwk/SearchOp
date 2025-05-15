export let SearchApiWebUrl: string;

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AppConfiguration {
  constructor(private http: HttpClient) {}

  public load() {
    return new Promise(resolve => {
      this.http
        .get("assets/ngx-config.json", { withCredentials: true })
        .pipe(map(res => res as NgxConfig))
        .subscribe(config => {
          this.setConfigSettings(config);
          resolve(true);
        });
    });
  }

  public setConfigSettings(config: NgxConfig) {
    SearchApiWebUrl = config.searchApiWebUrl;
  }
}

export interface NgxConfig {
  searchApiWebUrl: string;
}
