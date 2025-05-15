export class SearchRequest {
    url: string;
    searchTerm: string;  
    usePlaywright: boolean;  

    constructor(url: string, searchTerm: string, usePlaywright: boolean) {
        this.url = url;
        this.searchTerm = searchTerm;
        this.usePlaywright = usePlaywright;
    }  
}