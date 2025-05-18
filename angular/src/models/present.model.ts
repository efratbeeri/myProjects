export interface Present {
    id: number;
    name: string;
    price: number;
    kategory: string;
    image: string;
    donorsId: number;
    donorName?:string;
    isDrawn?:boolean;
    winnerName?:string
  }
