import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchService } from "../services/search.service";
import { ToastrService } from "ngx-toastr";
import { SearchEngine, SearchRequest, SearchResult } from '../model';
import { HttpClient } from "@angular/common/http";
import { HttpErrorResponse } from '@angular/common/http';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { ScatterChartComponent } from '../components/scatter-chart/scatter-chart.component';
import { SpinnerOverlayComponent } from '../components/spinneroverlay/spinneroverlay.component';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, MatAutocompleteModule, MatInputModule, MatButtonModule, MatCheckboxModule, FormsModule, ScatterChartComponent, SpinnerOverlayComponent],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss',
  providers: [SearchService, ToastrService, HttpClient]
})
export class SearchComponent implements OnInit {

  url: string = "";
  searchTerm: string = "";
  usePlaywright: boolean = false;

  searchResults: SearchResult[] = [];
  searchEngines: SearchEngine[] = [];
  filteredOptions: SearchEngine[] = [];
  loading: boolean = true;

  constructor(
    private searchService: SearchService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.getSearchEngines();
  }

  getSearchEngines() {
    this.searchService.getSearchEngine().subscribe({ 
        next:  (response) => {
          this.searchEngines = response.data;
          this.filteredOptions = [...this.searchEngines];
        },
        error: (error) => {
          let msg = this.getError(error);
          this.toastrService.error("Error occurred: " + msg, "", {
            positionClass: "toast-top-full-width"
          });
          this.completedAction();
        },
        complete: () => {
          this.completedAction();
        }
    });
  }

  onOptionSelected(event: any): void {
    console.log('Option selected:', event.option.value);
  }

  filterOptions(): void {
    this.filteredOptions = this.searchEngines.filter(option =>
      option.engineDescription.toLowerCase().includes(this.url.toLowerCase())
    );
  }

  submitForm() {
    this.loading = true;
    const formData = {
      searchTerm: this.searchTerm,
      url: this.url,
      usePlaywright: this.usePlaywright
    }
  
    if (formData.searchTerm.trim() && formData.url.trim())
    {
      var request = new SearchRequest(formData.url, formData.searchTerm, formData.usePlaywright)

      this.searchService.getSearchData(request).subscribe({
        next: (response) => {
            this.searchResults = response.data;
        },
        error: (error) => {
          let msg = this.getError(error);
          this.toastrService.error("Error occurred: " + msg, "", {
            positionClass: "toast-top-full-width"
          });
          this.completedAction();
        },
        complete: () => {
          this.completedAction();
        }
      })
    } else {
      this.completedAction();
    }
  }

  completedAction() {
    this.loading = false;
  }

  getError(error: any) {
    let msg = "";
    if (error instanceof HttpErrorResponse) {
      msg = error.message;
    } else {
      msg = error;
    }
    return msg;
  }

}
