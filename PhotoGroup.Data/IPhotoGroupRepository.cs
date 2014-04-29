using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Data
{
    public interface IPhotoGroupRepository
    {
		bool SaveAll();
	    Photo GetPhoto(int id);
	    IQueryable<Photo> GetAllPhotosForUser(int userId);
		IQueryable<Photo> GetPhotosForAlbum(int albumId);
		IQueryable<Photo> GetAllPhotos();
	    IQueryable<Album> GetAlbumsForUser(int userId);
	    Album GetAlbum(int id);
	    User GetUserInfo(int id);
	    IQueryable<User> GetAllUsers();
	    bool Insert(Album album);
	    bool DeleteAlbum(int id);
    }
}
