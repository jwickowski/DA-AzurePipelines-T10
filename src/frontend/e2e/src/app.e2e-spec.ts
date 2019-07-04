import { AppPage } from './app.po';
import { browser, logging } from 'protractor';

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    browser.waitForAngularEnabled(false); 
  });

  it('should display welcome message', async (done) => {
    await page.navigateTo();
    var itemsBeforeAdding = await page.getItemsCount();

    await page.getInput().sendKeys('sample item');
    await page.getButton().click();
    await browser.sleep(1500);
    await browser.refresh();
    await browser.sleep(1500);
    var itemsAfterAdding = await page.getItemsCount();
    expect(itemsAfterAdding).toBe(itemsBeforeAdding +1);
    done();
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });
});
