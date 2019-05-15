export class Subcription  {

    public id: number;
    public email: string;
    public reason: string;
    public referrer: string;
    public startDate: Date;

    constructor(subcription: Subcription) {

        if (!subcription) {
            return;
        }

        this.id = subcription.id;
        this.email = subcription.email;
        this.reason = subcription.reason;
        this.referrer = subcription.referrer;
        this.startDate = subcription.startDate;
    }
}