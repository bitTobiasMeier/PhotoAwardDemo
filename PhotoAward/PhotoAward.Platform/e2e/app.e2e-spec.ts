import { PhotoAwardClientPage } from './app.po';

describe('photo-award-client App', function() {
  let page: PhotoAwardClientPage;

  beforeEach(() => {
    page = new PhotoAwardClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('pac works!');
  });
});
