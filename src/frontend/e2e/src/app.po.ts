import { browser, by, element } from 'protractor';
import { Driver } from 'selenium-webdriver/opera';

export class AppPage {
  navigateTo() {
    return browser.get(browser.baseUrl) as Promise<any>;
  }

getInput() {
  return element(by.className('to-do__to-do-list-view-component__input'));
}

getButton(){
  return element(by.className('to-do__to-do-list-view-component__button'));
}
  

  async getItemsCount(){
    return await element.all(by.className('to-do__to-do-list-view-component__list__item')).count();
  }
}
