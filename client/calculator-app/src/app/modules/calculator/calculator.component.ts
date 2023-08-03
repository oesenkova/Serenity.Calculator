import {Component} from '@angular/core';
import CalculatorService from "../../services/calculator/calculator-servise.service";
import {CalculationProviderType, defaultCalculationResponseModel, ICalculationResponseModel} from "../../models/calculator/calculator.models";

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css'],
  providers: [CalculatorService]
})
export class CalculatorComponent {
  definition: string = '0';
  calculationResult: ICalculationResponseModel = defaultCalculationResponseModel();

  selectedCalculationProvider: CalculationProviderType = CalculationProviderType.LocalProvider;
  calculationProviders = [
    {label: 'Local provider', value: CalculationProviderType.LocalProvider},
    {label: 'MathJS', value: CalculationProviderType.MathJS},
    {label: 'MathEval', value: CalculationProviderType.MathEval}
  ];

  private digitRegularExpression: string = "^[0-9]+$";

  constructor(private calculatorService: CalculatorService) {
  }

  addNumber(key: string): void {
    let lastSymbol: string = this.definition[this.definition.length - 1];

    if (this.calculationResult.result != '' && this.definition.match(this.digitRegularExpression)) {
      this.definition = key;
      this.calculationResult.result = '';
    } else if (!this.isClosedBrackets(lastSymbol)) {
      this.definition = this.definition === '0' ? key : this.definition += key;
    }
  }

  addDecimal(): void {
    let operatorsCount = [...this.definition].filter(x => ['+', '-', '*', '/'].includes(x)).length;
    let dotsCount = [...this.definition].filter(x => x === '.').length;

    let lastSymbol: string = this.definition[this.definition.length - 1];

    if (operatorsCount >= dotsCount && !this.isClosedBrackets(lastSymbol) && !this.isOpenedBrackets(lastSymbol) && !this.isOperator(lastSymbol)) {
      this.definition += '.';
    }
  }

  addOperator(key: string): void {
    let length: number = this.definition.length;
    let lastSymbol: string = this.definition[length - 1];

    if (this.isOpenedBrackets(lastSymbol) && key != '-' || lastSymbol === '-' && (length == 1 || this.isOpenedBrackets(this.definition[length - 2])) && this.isOperator(key)) {
      return;
    } else if (this.isOperator(lastSymbol) && this.isOperator(key)) {
      this.clearLast();
    }

    this.definition = key === '-' && this.definition === '0' ? this.definition = key : this.definition += key;
  }

  addOpenedBrackets(): void {
    let lastSymbol: string = this.definition[this.definition.length - 1];

    if (this.isOperator(lastSymbol) || this.isOpenedBrackets(lastSymbol) || this.definition === '0') {
      this.definition = this.definition === '0' ? '(' : this.definition += '(';
    }
  }

  addClosedBrackets(): void {
    let lastSymbol: string = this.definition[this.definition.length - 1];

    let openedBracketsCount = [...this.definition].filter(x => x === '(').length;
    let closedBracketsCount = [...this.definition].filter(x => x === ')').length;

    if (!this.isOperator(lastSymbol) && this.definition.includes('(') && openedBracketsCount > closedBracketsCount) {
      this.definition = this.definition += ')';
    }
  }

  async calculate() {
    this.checkBracketsCount();

    await this.calculatorService.calculate({
      definition: this.definition,
      providerType: Number(this.selectedCalculationProvider)
    });

    this.calculationResult = this.calculatorService.getCalculationResult();
    this.definition = this.calculationResult.result;
  }

  clearAll(): void {
    this.definition = '0';
    this.calculationResult = defaultCalculationResponseModel();
  }

  clearLast(): void {
    this.definition = this.definition.slice(0, -1);
    this.calculationResult = defaultCalculationResponseModel();
  }

  private isOperator(value: string): boolean {
    return ['+', '-', '*', '/'].includes(value);
  }

  private isOpenedBrackets(value: string): boolean {
    return value === '(';
  }

  private isClosedBrackets(value: string): boolean {
    return value === ')';
  }

  private checkBracketsCount(): void {
    const openedBracketsCount = [...this.definition].filter(x => x === '(').length;
    const closedBracketsCount = [...this.definition].filter(x => x === ')').length;

    if (openedBracketsCount > closedBracketsCount) {
      let subtraction = openedBracketsCount - closedBracketsCount;

      while (subtraction != 0) {
        this.definition += ')';
        subtraction--;
      }
    }
  }
}
