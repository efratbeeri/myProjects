import { Purchases } from "./purchases.module";

export class Cart{
  id!: number;
  userId!:number;
  final!:boolean;
  purchasesList!: Purchases[]
  ;}