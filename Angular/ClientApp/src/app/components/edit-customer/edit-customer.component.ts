import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators, FormControl } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { CustomerService } from "../../services/customer.service";


@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
/** edit-customer component*/
export class EditCustomerComponent implements OnInit {
  customerForm: FormGroup;
  customerID: number;
  errorMessage: any;

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute, private _customerService: CustomerService,
    private _router: Router) {
    if (this._avRoute.snapshot.params["id"]) {
      this.customerID = this._avRoute.snapshot.params["id"];
    }
    this.customerForm = this._fb.group({
      customerID: 0,
      name: ["", [Validators.required]],
      tfn: ["", [Validators.minLength(11), Validators.maxLength(11)]],
      address: ["", [Validators.maxLength(50)]],
      city: ["", [Validators.maxLength(40)]],
      state: ["", [Validators.minLength(3), Validators.maxLength(3), Validators.pattern("[A-Za-z]{3}")]],
      postCode: [0, [Validators.maxLength(4), Validators.minLength(4), Validators.pattern("[0-9]{4}")]],
      phone: ["", [Validators.pattern("[+]614-[0-9]{4}-[0-9]{4}")]],
      accounts: [""],
      payees: [""]
    });
  }

  ngOnInit(): void {
    if (this.customerID > 0) {
      this._customerService.getCustomerById(this.customerID).subscribe(resp => {
        this.customerForm.setValue(resp)
      }, error => this.errorMessage = error);
    }
  }

  save() {
    if (!this.customerForm.valid) {
      return;
    }
    this._customerService.updateCustomer(this.customerForm.value).subscribe((data) => {
      this._router.navigate(["/fetch-customer"]);
    }, error => this.errorMessage = error);
  }

  cancel() {
    this._router.navigate(["/fetch-customer"]);
  }

  get name() {
    return this.customerForm.get("name");
  }

  get tfn() {
    return this.customerForm.get("tfn");
  }

  get address() {
    return this.customerForm.get("address");
  }

  get city() {
    return this.customerForm.get("city");
  }

  get state() {
    return this.customerForm.get("state");
  }

  get postCode() {
    return this.customerForm.get("postCode");
  }

  get phone() {
    return this.customerForm.get("phone");
  }
}
