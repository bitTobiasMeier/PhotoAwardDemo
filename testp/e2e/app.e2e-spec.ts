import { TestpPage } from './app.po';

describe('testp App', function() {
  let page: TestpPage;

  beforeEach(() => {
    page = new TestpPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
