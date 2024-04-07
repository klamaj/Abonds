import { SubInterest } from "./sub-interest.model";

export interface Interest {
    id: number;
    interestName: string;
    interestColor: string;
    subInterests: SubInterest[];
}