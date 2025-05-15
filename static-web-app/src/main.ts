import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { AppConfiguration } from './app/app.configuration';
import { APP_INITIALIZER } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export function initApp(appConfig: AppConfiguration) {
  return () => appConfig.load(); 
}

bootstrapApplication(AppComponent, {
  providers: [
    ...appConfig.providers,
    AppConfiguration,
    {
      provide: APP_INITIALIZER,
      useFactory: initApp,
      deps: [AppConfiguration],
      multi: true
    }, 
    provideAnimationsAsync()
  ]
})
.catch((err) => console.error(err));
