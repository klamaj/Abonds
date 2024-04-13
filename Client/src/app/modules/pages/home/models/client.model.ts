import { Contract } from "./contract.model";

export interface Client {
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    email: string;
    sex: string;
    status: number;
    matchedUserId: null | number;
    matchedUser: null | Client;
    id: number;
    contractId: number | null;
    contract: Contract | null;
    questionsSend: boolean;
    answeredQuestions: boolean;
    profileImagePath: string | null;
}