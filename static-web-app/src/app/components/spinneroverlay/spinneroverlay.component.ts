import { Component } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  standalone: true,
  imports: [MatProgressSpinnerModule],
  selector: 'app-spinneroverlay',
  templateUrl: './spinneroverlay.component.html'
})
export class SpinnerOverlayComponent {

}
