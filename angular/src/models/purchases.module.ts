import { Present } from "./present.model";

export class Purchases{
  id!:string;
  amount!:number;
  presentID?:number;
  present?: Present | null;
  cartId?:number;
}