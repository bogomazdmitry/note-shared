export const actionRoutes = {
  authSignup: 'signup',
  authToken: '/connect/token',
  userInfo: '',
  userChangeInfo: '',
  noteUpdate: 'update-note',
  noteDesignUpdate: 'update-note-design',
  noteCreate: '',
  noteDelete: '',
  notesGet: '',
  notesSharedUsersEmails: 'shared-users-emails',
  notesAddSharedUser: 'add-shared-user',
  notesDeleteSharedUser: 'delete-shared-user',
  notesUpdateOrder: 'update-order',
  authCheckUniqueUserName: 'check-unique-user-name',
  authCheckUniqueEmail: 'check-unique-email',
};

export const hubsRoutes = {
  note: '/hub/note'
};

export const controllerRoutes = {
  auth: '/api/auth/',
  user: '/api/user/',
  notes: '/api/notes/',
  note: '/api/note/',
  notifications: '/api/notifications/',
};

export const authTokenRequestNames = {
  grantType: 'grant_type',
  clientId: 'client_id',
  username: 'username',
  password: 'password',
  scope: 'scope',
};

export const authTokenRequestValues = {
  password: 'password',
  scope: 'IdentityServerApi openid',
};
