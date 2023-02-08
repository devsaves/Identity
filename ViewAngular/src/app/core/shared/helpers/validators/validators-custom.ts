
import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";



export class ValidatorsCustom {

  constructor() { }

  static checkedBoxValidator(form: FormGroup, checked: boolean, controls: string[]) {
    //checked true all controls is required.
    const checkedValue: boolean = checked
    if (checkedValue) {
      controls.map(control => form.get(control).setValidators(Validators.required));
    }
    else {
      controls.map(control => {
        form.get(control).clearValidators();
        form.get(control).updateValueAndValidity();
        form.get(control).reset();
      })
    }
  }

  static selectValidator(form: FormGroup, selected: string, operators: string, wordApplyOperator: string, ctrls: string[]) {

    const selectedValue = selected.toLowerCase();
    const conditional = operators;
    const wordTest = wordApplyOperator.toLowerCase();
    const controls = ctrls;

    if (conditional === '==' || conditional === '===') {
      if (selectedValue === wordTest) {
        controls.map(control => {
          if (!form.get(control).value) {
            form.get(control).addValidators(Validators.required)
          }
        })
      }
      else {
        controls.map(control => {
          form.get(control).clearValidators();
          form.get(control).updateValueAndValidity();
          form.get(control).reset();
        })
      }
    }

    if (conditional === '!=') {
      if (selectedValue != wordTest) {
        controls.map(control => {
          if (!form.get(control).value) {
            form.get(control).addValidators(Validators.required)
            // console.log(form)
          }
        })
      }
      else {
        controls.map(control => {
          // console.log(control)
          form.get(control).clearValidators();
          form.get(control).updateValueAndValidity();
          // form.get(control).reset();
        })
      }
    }
  }

  static blurValidator(form: FormGroup, errorType: any[], control: string) {

    const errors = errorType;
    const ctrl = control;
    if (!form.get(ctrl).value) {
      errors.map(error => form.get(ctrl).setErrors(error))
    }

  }

  static atLeastOneValidationBlur(form: FormGroup, controls: string[]) {

    const ctrls = controls;
    //const error = errorType;

    ctrls.map(control => {
      if (!form?.get(control)?.value) {
        form?.get(control)?.setValidators(Validators.required);
      }
    })

    ctrls.map(control => {
      if (form?.get(control)?.value) {
        ctrls.map(ctrlToNull => {
          form?.get(ctrlToNull)?.clearValidators();
          form?.get(ctrlToNull)?.updateValueAndValidity();
        })
      }
    })
  }

  // static dateFormatValidator(aControl: AbstractControl) {
  //   console.log(aControl)
  //   if (aControl && aControl.value && !moment(aControl.value, 'DD-MM-YYYY', true).isValid()) {
  //     return { invalidDate: true }
  //   }
  //   return null;
  // }

  static minMaxLengthRequired(form: FormGroup, controlMax?: object) {

    const fieldMax = controlMax['maxField'];
    const quantityMax = controlMax['maxQuantity'];
    const fieldMaxIsRequired = controlMax['maxFieldIsRequired'];

    const fieldMin = controlMax['minField'];
    const fieldMinIsRequired = controlMax['minFieldIsRequired'];
    const quantityMin = controlMax['minQuantity'];

    if (fieldMax) {
      if (!fieldMaxIsRequired) {
        form?.get(fieldMax)?.setValidators(Validators.maxLength(quantityMax))
        form?.get(fieldMax)?.updateValueAndValidity();
      }

      if (fieldMaxIsRequired) {
        form?.get(fieldMax)?.setValidators([Validators.required, Validators.maxLength(quantityMax)])
        form?.get(fieldMax)?.updateValueAndValidity();
      }
    }
    if (fieldMin) {

      if (!fieldMinIsRequired) {
        form?.get(fieldMin)?.setValidators(Validators.minLength(quantityMin))
        form?.get(fieldMin)?.updateValueAndValidity();
      }
      if (fieldMinIsRequired) {
        form?.get(fieldMin)?.setValidators([Validators.required, Validators.minLength(quantityMin)])
        form?.get(fieldMin)?.updateValueAndValidity();
        //
      }
    }


    form?.get(fieldMax).updateValueAndValidity();
  }

  // static fieldCompare(source: string, target: string): ValidatorFn {
  //   return (control: AbstractControl): ValidationErrors | null => {
  //     const sourceCtrl = control.get(source);
  //     const targetCtrl = control.get(target);
  //     return sourceCtrl && targetCtrl && sourceCtrl.value !== targetCtrl.value ? { notIqual: true } : null;
  //   };
  // }

  confirm() {

  }

  // static fieldComparePerform(formGroup:FormGroup){
  //   formGroup.get('password').setErrors({'noIqual': true})
  //    formGroup.get('confirmPassword').setErrors({'noIqual': true})
  //   }

  static fieldCompare(formGroup: FormGroup, ctrlSource: string, ctrlTarget: string) {

    if (formGroup.get(ctrlSource).value === formGroup.get(ctrlTarget).value) {
      formGroup.get(ctrlSource).setErrors(null);
      formGroup.get(ctrlTarget).setErrors(null);
      //formGroup.updateValueAndValidity();
    }
    else{
      formGroup.get(ctrlSource).setErrors({ 'noIqual': true });
      formGroup.get(ctrlTarget).setErrors({ 'noIqual': true });
      //formGroup.updateValueAndValidity();
    }
  }
  // static fieldCompare(formGroup: FormGroup) {
  //   return formGroup.get('password').value === formGroup.get('confirmPassword').value
  //     ? null : {'noIqual': true};
  // }



}


