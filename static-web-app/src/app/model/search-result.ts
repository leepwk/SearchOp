export class SearchResult {
    rank: number;
    entryDate: string;  
    url: string;
    searchTerm: string  

    constructor(rank: number, entryDate: string, url: string, searchTerm: string) {
        this.rank = rank;
        this.entryDate = entryDate;
        this.url = url;
        this.searchTerm = searchTerm;
    }  
}