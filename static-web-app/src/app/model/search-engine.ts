export class SearchEngine {
    engineId: number;
    engineDescription: string;  

    constructor(engineId: number, engineDescription: string) {
        this.engineId = engineId;
        this.engineDescription = engineDescription;
    }  
}