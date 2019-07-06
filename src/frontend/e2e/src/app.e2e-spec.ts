import { AppPage } from './app.po';
import { browser, logging } from 'protractor';

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    browser.waitForAngularEnabled(false);
  });

  it('should increase the number of items after adding', async (done) => {
    page.navigateTo();

    await browser.sleep(5000);
    var itemsBeforeAdding = await page.getItemsCount();

    page.getInput().sendKeys('sample item');
    page.getButton().click();
    await browser.sleep(1000);
    await browser.refresh();

    await browser.sleep(3000);
    var itemsAfterAdding = await page.getItemsCount();
    expect(itemsAfterAdding).toBe(itemsBeforeAdding + 1);
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
