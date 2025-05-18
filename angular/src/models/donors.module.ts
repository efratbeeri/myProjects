export class Donors {
  id!: number;   // באותיות קטנות, תואם ל-API
  name!: string; // באותיות קטנות, תואם ל-API
  email?: string ; 
  phone?: string;   
  donation?: {
      $id?: string;
      $values?: any[];
  };
}
